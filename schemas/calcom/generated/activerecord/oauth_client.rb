class OAuthClient < ApplicationRecord
  self.table_name = "OAuthClient"
  self.primary_key = "clientId"

  attribute :isTrusted, default: false
  alias_attribute :created_at, :createdAt

  enum :clientType, { confidential: "confidential", public: "public" }, default: :confidential, prefix: true,
                                                                        validate: true
  enum :status, { pending: "pending", approved: "approved", rejected: "rejected" }, default: :approved, validate: true

  validates :redirectUri, presence: true
  validates :name, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :o_auth_clients
  has_many :access_codes, foreign_key: "clientId", inverse_of: :client, dependent: :destroy
end
