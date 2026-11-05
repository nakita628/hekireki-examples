class ScimIdentityLink < ApplicationRecord
  self.table_name = "ScimIdentityLink"

  attribute :id, default: -> { Cuid.generate }

  validates :connectionId, presence: true, uniqueness: { scope: :externalId }
  validates :externalId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :scim_identity_links
end
