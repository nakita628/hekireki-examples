class OAuthAppAuthorized < ApplicationRecord
  self.table_name = "oauth_app_authorized"

  attribute :id, default: -> { Cuid.generate }

  validates :user_id, presence: true, uniqueness: { scope: :client_id }
  validates :authorized_time, presence: true

  belongs_to :o_auth_app, foreign_key: "client_id", primary_key: "client_id", inverse_of: :authorized
end
