use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "provider_calendars")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub calendar_connection_id: String,
    pub provider_calendar_id: String,
    pub name: String,
    #[sea_orm(column_name = "primary", default_value = false)]
    pub is_primary: bool,
    pub time_zone: Option<String>,
    #[sea_orm(column_name = "selected", default_value = false)]
    pub is_selected: bool,
    #[sea_orm(column_name = "deleted", default_value = false)]
    pub is_deleted: bool,
    #[sea_orm(column_name = "writable", default_value = false)]
    pub is_writable: bool,
    pub provider_data: Option<Json>,
    pub last_synced_at: DateTime,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::calendar_connection::Entity",
        from = "Column::CalendarConnectionId",
        to = "super::calendar_connection::Column::Id",
        on_delete = "Cascade"
    )]
    CalendarConnection,
    #[sea_orm(has_many = "super::user::Entity")]
    UserDefaultDestination,
}

impl Related<super::calendar_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CalendarConnection.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserDefaultDestination.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.last_synced_at.is_not_set() {
            self.last_synced_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
