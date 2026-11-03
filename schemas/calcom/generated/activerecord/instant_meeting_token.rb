class InstantMeetingToken < ApplicationRecord
  self.table_name = "InstantMeetingToken"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true
  validates :bookingId, uniqueness: { allow_nil: true }

  belongs_to :team, foreign_key: "teamId", inverse_of: :instant_meeting_tokens
  belongs_to :booking, foreign_key: "bookingId", optional: true, inverse_of: :instant_meeting_token
end
