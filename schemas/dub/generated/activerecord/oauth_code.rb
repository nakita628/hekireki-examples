class OAuthCode < ApplicationRecord
  self.table_name = "OAuthCode"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :code, presence: true, uniqueness: true
  validates :redirectUri, presence: true
  validates :expiresAt, presence: true

  belongs_to :o_auth_app, foreign_key: "clientId", primary_key: "clientId", inverse_of: :o_auth_codes
  belongs_to :user, foreign_key: "userId", inverse_of: :o_auth_codes
  belongs_to :project, foreign_key: "projectId", inverse_of: :o_auth_codes
end
