class OauthConsent < ApplicationRecord
  self.table_name = "OauthConsent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  attribute :scopes, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :clientId, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :oauthconsents
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :oauth_consent_records
end
