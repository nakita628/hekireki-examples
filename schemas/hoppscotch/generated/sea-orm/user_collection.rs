use super::req_type::ReqType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "UserCollection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "parentID")]
    pub parent_id: Option<String>,
    #[sea_orm(column_name = "userUid")]
    pub user_uid: String,
    pub title: String,
    pub r#type: ReqType,
    #[sea_orm(column_name = "orderIndex")]
    pub order_index: i32,
    pub data: Option<Json>,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedOn", column_type = "TimestampWithTimeZone")]
    pub updated_on: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user_collection::Entity",
        from = "Column::ParentID",
        to = "super::user_collection::Column::Id",
        on_delete = "Cascade"
    )]
    Parent,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserUid",
        to = "super::user::Column::Uid",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        has_many = "super::user_collection::Entity",
        from = "Column::Id",
        to = "super::user_collection::Column::ParentID"
    )]
    Children,
    #[sea_orm(has_many = "super::user_request::Entity")]
    Requests,
}

impl Related<super::user_collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Parent.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::user_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Requests.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        if !self.updated_on.is_set() {
            self.updated_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
