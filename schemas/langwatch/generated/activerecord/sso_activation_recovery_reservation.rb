class SsoActivationRecoveryReservation < ApplicationRecord
  self.table_name = "SsoActivationRecoveryReservation"
  self.primary_key = "commandId"

  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true, uniqueness: { scope: :connectionId }
  validates :connectionId, presence: true
end
