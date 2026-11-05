class Session < ApplicationRecord
  self.table_name = "Session"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :emailOtp, default: false
  attribute :emailOtpVersion, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :sessionToken, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :sessions
  belongs_to :active_organization, class_name: "Organization", foreign_key: "activeOrganizationId", optional: true,
                                   inverse_of: :sessions
  has_many :oauth_refresh_token_records, class_name: "OauthRefreshToken", foreign_key: "sessionId", inverse_of: :session,
                                         dependent: :nullify
  has_many :oauth_access_token_records, class_name: "OauthAccessToken", foreign_key: "sessionId", inverse_of: :session,
                                        dependent: :nullify
end
