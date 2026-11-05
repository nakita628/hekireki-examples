class OauthClientAssertion < ApplicationRecord
  self.table_name = "OauthClientAssertion"

  attribute :id, default: -> { Cuid.generate }

  validates :expiresAt, presence: true
end
