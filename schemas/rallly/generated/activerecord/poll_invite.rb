class PollInvite < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :email, presence: true, uniqueness: { scope: :poll_id }
  validates :token, presence: true, uniqueness: true
  validates :participant_id, uniqueness: { allow_nil: true }

  belongs_to :poll, inverse_of: :invites
  belongs_to :participant, optional: true, inverse_of: :invite
end
