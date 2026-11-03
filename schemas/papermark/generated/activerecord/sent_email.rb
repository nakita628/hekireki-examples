class SentEmail < ApplicationRecord
  self.table_name = "SentEmail"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :marketing, default: false
  alias_attribute :created_at, :createdAt

  enum :type, { first_day_domain_reminder_email: "FIRST_DAY_DOMAIN_REMINDER_EMAIL", first_domain_invalid_email: "FIRST_DOMAIN_INVALID_EMAIL", second_domain_invalid_email: "SECOND_DOMAIN_INVALID_EMAIL", first_trial_end_reminder_email: "FIRST_TRIAL_END_REMINDER_EMAIL", final_trial_end_reminder_email: "FINAL_TRIAL_END_REMINDER_EMAIL" },
       validate: true

  validates :recipient, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :sent_emails
end
