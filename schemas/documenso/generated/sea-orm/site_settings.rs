use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SiteSettings")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    pub data: Json,
    #[sea_orm(column_name = "lastModifiedByUserId")]
    pub last_modified_by_user_id: Option<i32>,
    #[sea_orm(column_name = "lastModifiedAt")]
    pub last_modified_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::LastModifiedByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    LastModifiedByUser,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LastModifiedByUser.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.last_modified_at.is_not_set() {
            self.last_modified_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
