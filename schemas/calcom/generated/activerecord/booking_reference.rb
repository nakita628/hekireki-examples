class BookingReference < ApplicationRecord
  self.table_name = "BookingReference"
  self.inheritance_column = nil

  validates :type, presence: true
  validates :uid, presence: true

  belongs_to :booking, foreign_key: "bookingId", optional: true, inverse_of: :references
  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :references
  belongs_to :delegation_credential, foreign_key: "delegationCredentialId", optional: true,
                                     inverse_of: :booking_references
end
