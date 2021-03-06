import VPlay 2.0
import QtQuick 2.0

EntityBase {
    entityType: "TienSkill3"
    id:idSkillTien3

    SpriteSequenceVPlay {
        id: idtienSkill3
        SpriteVPlay {
            name: "tienSkill1"
            frameCount: 2
            frameRate: 6
            frameWidth: 18
            frameHeight: 17
            reverse: true
            source: "../assets/skills/tienSkill3.png"
        }
    }

    property point destination
    property point enemyDirect
    property int moveDuration

    PropertyAnimation on x {
        from: enemyDirect.x
        to: destination.x //cho thoat khoi man hinh
        duration: moveDuration
    }

    PropertyAnimation on y {
        from: enemyDirect.y + 25
        to: destination.y //cho thoat khoi man hinh
        duration: moveDuration
    }

    BoxCollider {
        anchors.fill: idtienSkill3
        collisionTestingOnlyMode: true
        fixture.onBeginContact: {
            var collidedEntity = other.getBody().target
            if(collidedEntity.entityType === "skillPlayer" || collidedEntity.entityType === "skillAuraBlast") {
                collidedEntity.removeEntity() //xoa dan
            }
            if(collidedEntity.entityType === "skillKamehameha") {
                removeEntity() //xoa enemy
            }
        }
    }

    Timer{
        running:true
        repeat: true
        interval: 100
        onTriggered: {
            if(idSkillTien3.x <= rtgGamePlay.x*1.25)
                removeEntity()
        }
    }
}// EntityBase
