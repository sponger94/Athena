#!/bin/sh

CI_API_NAME=$1

main () {
    case "$CI_API_NAME" in
        pomodoro_api) 
			build_pomodoro_api ;;
        tasks_api) 
			build_tasks_api ;;
        all)
            build_pomodoro_api
            build_tasks_api;;
        *) 
			echo "Invalid api name!" ;;
    esac
}

build_pomodoro_api(){
    cd ../Services/Pomodoro/
    sh build.sh
}

build_tasks_api(){
    cd ../Services/Tasks/
    sh build.sh
}

main "$@"