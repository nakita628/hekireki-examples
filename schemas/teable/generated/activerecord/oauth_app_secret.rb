class OAuthAppSecret < ApplicationRecord
  self.table_name = "oauth_app_secret"

  attribute :id, default: -> { Cuid.generate }

  validates :secret, presence: true, uniqueness: true
  validates :masked_secret, presence: true
  validates :created_by, presence: true

  belongs_to :o_auth_app, foreign_key: "client_id", primary_key: "client_id", inverse_of: :secrets
  has_many :tokens, class_name: "OAuthAppToken", foreign_key: "app_secret_id", inverse_of: :o_auth_app_secret,
                    dependent: :destroy
end
