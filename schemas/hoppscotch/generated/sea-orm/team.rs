use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Team")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::team_collection::Entity")]
    Collections,
    #[sea_orm(has_many = "super::team_environment::Entity")]
    Environments,
    #[sea_orm(has_many = "super::team_invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::team_member::Entity")]
    Members,
    #[sea_orm(has_many = "super::team_request::Entity")]
    Requests,
}

impl Related<super::team_collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Collections.def()
    }
}

impl Related<super::team_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environments.def()
    }
}

impl Related<super::team_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::team_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::team_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Requests.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
