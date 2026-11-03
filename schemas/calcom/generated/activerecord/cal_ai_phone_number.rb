class CalAiPhoneNumber < ApplicationRecord
  self.table_name = "CalAiPhoneNumber"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :subscriptionStatus, { active: "ACTIVE", past_due: "PAST_DUE", cancelled: "CANCELLED", incomplete: "INCOMPLETE", incomplete_expired: "INCOMPLETE_EXPIRED", trialing: "TRIALING", unpaid: "UNPAID" },
       validate: { allow_nil: true }

  validates :phoneNumber, presence: true, uniqueness: true
  validates :provider, presence: true
  validates :providerPhoneNumberId, uniqueness: { allow_nil: true }
  validates :stripeSubscriptionId, uniqueness: { allow_nil: true }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :cal_ai_phone_numbers
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :cal_ai_phone_numbers
  belongs_to :inbound_agent, class_name: "Agent", foreign_key: "inboundAgentId", optional: true,
                             inverse_of: :inbound_phone_numbers
  belongs_to :outbound_agent, class_name: "Agent", foreign_key: "outboundAgentId", optional: true,
                              inverse_of: :outbound_phone_numbers
end
