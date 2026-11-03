class AccessCode < ApplicationRecord
  self.table_name = "AccessCode"

  attribute :scopes, default: -> { [] }
  attribute :codeChallengeMethod, default: "S256"

  validates :code, presence: true
  validates :expiresAt, presence: true

  belongs_to :client, class_name: "OAuthClient", foreign_key: "clientId", optional: true, inverse_of: :access_codes
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :access_codes
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :access_codes
end
