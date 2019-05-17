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
    sh src/Services/Pomodoro/test.sh
}

test_tasks_api(){
    sh src/Services/Tasks/test.sh
}
  
main "$@"