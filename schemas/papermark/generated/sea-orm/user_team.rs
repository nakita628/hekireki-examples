use super::role::Role;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "UserTeam")]
pub struct Model {
    #[sea_orm(default_value = "MEMBER")]
    pub role: Role,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "userId")]
    pub user_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "blockedAt")]
    pub blocked_at: Option<DateTime>,
    #[sea_orm(column_name = "notificationPreferences")]
    pub notification_preferences: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
