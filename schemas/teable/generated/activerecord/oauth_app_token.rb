class OAuthAppToken < ApplicationRecord
  self.table_name = "oauth_app_token"

  attribute :id, default: -> { Cuid.generate }

  validates :refresh_token_sign, presence: true, uniqueness: true
  validates :expired_time, presence: true
  validates :created_by, presence: true

  belongs_to :o_auth_app_secret, foreign_key: "app_secret_id", optional: true, inverse_of: :tokens
  belongs_to :o_auth_app, foreign_key: "client_id", primary_key: "client_id", inverse_of: :tokens
end
