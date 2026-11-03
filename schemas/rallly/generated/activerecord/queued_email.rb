class QueuedEmail < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :attempts, default: 0

  enum :status, { pending: "pending", sent: "sent", skipped: "skipped", failed: "failed" }, default: :pending,
                                                                                            validate: true

  validates :kind, presence: true
  validates :subject_id, presence: true
  validates :batch_id, presence: true

  belongs_to :user, inverse_of: :queued_emails
end
