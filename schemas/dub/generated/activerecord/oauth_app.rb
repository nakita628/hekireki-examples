class OAuthApp < ApplicationRecord
  self.table_name = "OAuthApp"

  attribute :pkce, default: false

  validates :integrationId, uniqueness: true
  validates :clientId, presence: true, uniqueness: true
  validates :hashedClientSecret, presence: true
  validates :partialClientSecret, presence: true
  validates :redirectUris, exclusion: { in: [ nil ] }

  belongs_to :integration, foreign_key: "integrationId", optional: true, inverse_of: :o_auth_app
  has_many :o_auth_codes, class_name: "OAuthCode", foreign_key: "clientId", primary_key: "clientId",
                          inverse_of: :o_auth_app, dependent: :destroy
end
