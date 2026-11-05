class ScimIdentityTombstone < ApplicationRecord
  self.table_name = "scimIdentityTombstone"

  attribute :id, default: -> { Cuid.generate }

  validates :connectionId, presence: true
  validates :provisioningDomainId, presence: true
  validates :externalId, presence: true
  validates :externalIdKey, presence: true, uniqueness: true
  validates :externalIdKey, uniqueness: true
  validates :profile, presence: true
  validates :deletedAt, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_identity_tombstones
end
