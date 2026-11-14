class SignInProviderMount < ApplicationRecord
  self.table_name = "SignInProviderMount"

  attribute :id, default: "self"
  alias_attribute :updated_at, :updatedAt

  validates :mounted, inclusion: { in: [ true, false ] }
  validates :lastAppliedEventId, presence: true
end
