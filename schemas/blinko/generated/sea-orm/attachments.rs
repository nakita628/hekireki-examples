use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "attachments")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_name = "isShare", default_value = false)]
    pub is_share: bool,
    #[sea_orm(column_name = "sharePassword", default_value = "")]
    pub share_password: String,
    #[sea_orm(default_value = "")]
    pub name: String,
    #[sea_orm(default_value = "")]
    pub path: String,
    #[sea_orm(column_type = "Decimal(None)", default_value = 0)]
    pub size: Decimal,
    #[sea_orm(default_value = "")]
    pub r#type: String,
    #[sea_orm(column_name = "noteId")]
    pub note_id: Option<i32>,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<i32>,
    #[sea_orm(column_name = "sortOrder", default_value = 0)]
    pub sort_order: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "perfixPath", default_value = "")]
    pub perfix_path: Option<String>,
    pub depth: Option<i32>,
    pub metadata: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::notes::Entity",
        from = "Column::NoteId",
        to = "super::notes::Column::Id"
    )]
    Note,
    #[sea_orm(
        belongs_to = "super::accounts::Entity",
        from = "Column::AccountId",
        to = "super::accounts::Column::Id"
    )]
    Account,
}

impl Related<super::notes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Note.def()
    }
}

impl Related<super::accounts::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.fixed_offset());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
