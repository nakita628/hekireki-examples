class OauthClientAssertion < ApplicationRecord
  self.table_name = "oauthClientAssertion"

  attribute :id, default: -> { Cuid.generate }

  validates :expiresAt, presence: true
end
