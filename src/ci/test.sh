#!/bin/sh

CI_API_NAME=$1

main () {
    
    case "$CI_API_NAME" in
        pomodoro_api) 
			test_pomodoro_api ;;
        tasks_api) 
			test_tasks_api ;;
        all)
            test_pomodoro_api
            test_tasks_api;;
        *) 
			echo "Invalid api name!" ;;
    esac
}

test_pomodoro_api() {
    cd ./Services/Pomodoro/
    sh test.sh
}

test_tasks_api(){
    cd ./Services/Tasks/
    sh test.sh
}
    
    cd ./services/menu.api/
    sh test.sh | tee output.txt
    cd -
    
    COVERAGE_RESULT=$(grep "Total Branch" ./services/menu.api/output.txt | tr -dc '[0-9]+\.[0-9]')
    BADGE_COLOR=$(get_coverage_result_badge_color $COVERAGE_RESULT)
    COVERAGE_FILE_NAME="${CI_API_NAME}_coverage.svg"
    
    ./ci/generate_badge.sh $COVERAGE_FILE_NAME "menu--api" "$COVERAGE_RESULT%25" $BADGE_COLOR
    ./ci/upload_badge_s3.sh $COVERAGE_FILE_NAME
    ./ci/sync_folder_s3.sh "$(pwd)/services/menu.api/coveragereport/" $CI_API_NAME
}

main "$@"