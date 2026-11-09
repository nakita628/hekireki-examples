class UserApiToken < ApplicationRecord
  self.table_name = "UserApiToken"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :hint, presence: true
  validates :hash, presence: true

  belongs_to :user, class_name: "UserProfile", foreign_key: "userId", inverse_of: :tokens
  belongs_to :oauth_client, class_name: "OAuthClient", foreign_key: "oauthClientId", optional: true, inverse_of: :tokens
  has_many :access_tokens, class_name: "OAuthAccessToken", foreign_key: "refreshTokenId", inverse_of: :refresh_token,
                           dependent: :restrict_with_error
end
