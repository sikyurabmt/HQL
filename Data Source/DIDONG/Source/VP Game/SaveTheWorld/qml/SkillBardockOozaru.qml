import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "SkillBardockOozaru"
    id: idSkillBardockOozaru
    MultiResolutionImage {
        id: skillImage
        source: "../assets/skills/barkdockOozaSkill.png"
    }

    // these values can then be set when a new projectile is created in the MouseArea below
    property point destination
    property point enemyDirect
    property int moveDuration

    PropertyAnimation on x {
        from: enemyDirect.x - skillImage.width + 10
        to: destination.x //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: enemyDirect.y
        to: destination.y //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: skillImage
        collisionTestingOnlyMode: true
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "player") {
                removeEntity()
            }
        }
    }
    Timer{
        running:true
        repeat: true
        onTriggered: {
            if(idSkillBardockOozaru.x <= rtgGamePlay.x)
                removeEntity()
        }
    }
}// EntityBase
