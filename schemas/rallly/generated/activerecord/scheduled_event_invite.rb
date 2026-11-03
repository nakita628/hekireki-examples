class ScheduledEventInvite < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :status, { pending: "pending", accepted: "accepted", declined: "declined", tentative: "tentative" },
       default: :pending, validate: true

  validates :uid, presence: true, uniqueness: true
  validates :invitee_name, presence: true
  validates :invitee_email, presence: true, uniqueness: { scope: :scheduled_event_id }

  belongs_to :scheduled_event, inverse_of: :invites
  belongs_to :user, foreign_key: "invitee_id", optional: true, inverse_of: :scheduled_event_invites
end
