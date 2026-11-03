use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DSyncTeamGroupMapping")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: i32,
    #[sea_orm(column_name = "teamId")]
    pub team_id: i32,
    #[sea_orm(column_name = "directoryId")]
    pub directory_id: String,
    #[sea_orm(column_name = "groupName")]
    pub group_name: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::dsync_data::Entity",
        from = "Column::DirectoryId",
        to = "super::dsync_data::Column::DirectoryId",
        on_delete = "Cascade"
    )]
    Directory,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::dsync_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Directory.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
