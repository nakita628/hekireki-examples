class IdentitySignInSwitch < ApplicationRecord
  self.table_name = "IdentitySignInSwitch"
  self.primary_key = "userId"

  attribute :pinned, default: false
  alias_attribute :updated_at, :updatedAt

  validates :onIdentifiers, inclusion: { in: [ true, false ] }
  validates :switchedAt, presence: true
end
