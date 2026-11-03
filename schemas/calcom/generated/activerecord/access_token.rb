class AccessToken < ApplicationRecord
  self.table_name = "AccessToken"

  alias_attribute :created_at, :createdAt

  validates :secret, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :owner, class_name: "User", foreign_key: "userId", inverse_of: :access_token
  belongs_to :client, class_name: "PlatformOAuthClient", foreign_key: "platformOAuthClientId",
                      inverse_of: :access_tokens
end
