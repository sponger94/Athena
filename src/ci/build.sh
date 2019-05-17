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
    sh src/Services/Pomodoro/build.sh
}

build_tasks_api(){
    sh src/Services/Tasks/build.sh
}

main "$@"