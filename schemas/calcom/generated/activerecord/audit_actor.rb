class AuditActor < ApplicationRecord
  self.table_name = "AuditActor"
  self.inheritance_column = nil

  attribute :id, default: -> { SecureRandom.uuid_v7 }
  alias_attribute :created_at, :createdAt

  enum :type, { user: "user", guest: "guest", attendee: "attendee", system: "system", app: "app" }, prefix: true,
                                                                                                    validate: true

  validates :userUuid, uniqueness: { allow_nil: true }
  validates :userUuid, uniqueness: { allow_nil: true }
  validates :attendeeId, uniqueness: { allow_nil: true }
  validates :attendeeId, uniqueness: { allow_nil: true }
  validates :credentialId, uniqueness: { allow_nil: true }
  validates :credentialId, uniqueness: { allow_nil: true }
  validates :email, uniqueness: { allow_nil: true }
  validates :email, uniqueness: { allow_nil: true }
  validates :phone, uniqueness: { allow_nil: true }
  validates :phone, uniqueness: { allow_nil: true }

  has_many :booking_audits, foreign_key: "actorId", inverse_of: :actor, dependent: :restrict_with_error
end
