class OauthAccessToken < ApplicationRecord
  self.table_name = "oauthAccessToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :scopes, default: -> { [] }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :access_tokens
  belongs_to :session, foreign_key: "sessionId", optional: true, inverse_of: :oauth_access_tokens
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :oauth_access_tokens
  belongs_to :refresh, class_name: "OauthRefreshToken", foreign_key: "refreshId", optional: true,
                       inverse_of: :access_tokens
end
