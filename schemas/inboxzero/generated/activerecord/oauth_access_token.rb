class OauthAccessToken < ApplicationRecord
  self.table_name = "OauthAccessToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  attribute :scopes, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :token, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :oauthaccesstokens
  belongs_to :session, foreign_key: "sessionId", optional: true, inverse_of: :oauth_access_token_records
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :oauth_access_token_records
  belongs_to :refresh, class_name: "OauthRefreshToken", foreign_key: "refreshId", optional: true,
                       inverse_of: :oauthaccesstokens
end
