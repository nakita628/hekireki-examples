class OauthConsent < ApplicationRecord
  self.table_name = "oauthConsent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :scopes, default: -> { [] }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId", inverse_of: :consents
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :oauth_consents
end
