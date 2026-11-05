class ScimUser < ApplicationRecord
  self.table_name = "scimUser"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :connectionId, presence: true
  validates :provisioningDomainId, presence: true
  validates :connectionUserKey, presence: true, uniqueness: true
  validates :connectionUserKey, uniqueness: true
  validates :userName, presence: true
  validates :userNameKey, presence: true, uniqueness: true
  validates :userNameKey, uniqueness: true
  validates :primaryEmail, presence: true
  validates :workEmailValueIndex, presence: true
  validates :emailValueIndex, presence: true
  validates :displayName, presence: true
  validates :formattedName, presence: true
  validates :serializedEmails, presence: true
  validates :externalIdKey, uniqueness: { allow_nil: true }
  validates :externalIdKey, uniqueness: { allow_nil: true }
  validates :active, inclusion: { in: [ true, false ] }
  validates :orderKey, presence: true, uniqueness: true
  validates :orderKey, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_users
  has_many :scim_projection_grants, foreign_key: "scimUserId", inverse_of: :scim_user, dependent: :destroy
  has_many :scim_group_members, foreign_key: "scimUserId", inverse_of: :scim_user, dependent: :destroy
end
