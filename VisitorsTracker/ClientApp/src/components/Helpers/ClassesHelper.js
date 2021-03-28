import numberOfClasses from '../../constants/numberOfClasses';

export default function SetClassesStart(number, timeStart) {
    switch (number) {
        case numberOfClasses.First:
            timeStart.setHours(8);
            timeStart.setMinutes(20);
            return timeStart;
        case numberOfClasses.Second:
            timeStart.setHours(9);
            timeStart.setMinutes(50);
            return timeStart;
        case numberOfClasses.Third:
            timeStart.setHours(11);
            timeStart.setMinutes(30);
            return timeStart;
        case numberOfClasses.Fourth:
            timeStart.setHours(13);
            timeStart.setMinutes(0);
            return timeStart;
        case numberOfClasses.Fifth:
            timeStart.setHours(14);
            timeStart.setMinutes(40);
            return timeStart;
        case numberOfClasses.Sixth:
            timeStart.setHours(8);
            timeStart.setMinutes(20);
            return timeStart;
        case numberOfClasses.Seventh:
            timeStart.setHours(8);
            timeStart.setMinutes(20);
            return timeStart;
        case numberOfClasses.Eighth:
            timeStart.setHours(8);
            timeStart.setMinutes(20);
            return timeStart;
    }
}