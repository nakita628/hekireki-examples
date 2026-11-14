class SsoConnectionRegistrationSlot < ApplicationRecord
  self.table_name = "SsoConnectionRegistrationSlot"
  self.primary_key = %w[organizationId kind]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :connectionId, presence: true, uniqueness: true
  validates :commandId, presence: true
end
