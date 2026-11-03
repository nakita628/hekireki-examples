class Poll < ApplicationRecord
  attribute :deleted, default: false
  attribute :hide_participants, default: false
  attribute :hide_scores, default: false
  attribute :disable_comments, default: true
  attribute :require_participant_email, default: false
  attribute :allow_tentative_votes, default: true
  attribute :muted, default: false

  enum :status, { open: "open", closed: "closed", scheduled: "scheduled", canceled: "canceled" }, default: :open,
                                                                                                  prefix: true,
                                                                                                  validate: true
  enum :closed_reason, { auto: "auto", manual: "manual" }, validate: { allow_nil: true }
  enum :kind, { date: "date", time: "time" }, default: :date, validate: true

  validates :title, presence: true
  validates :event_id, uniqueness: { allow_nil: true }

  belongs_to :user, optional: true, inverse_of: :polls
  belongs_to :scheduled_event, optional: true, inverse_of: :polls
  belongs_to :space, optional: true, inverse_of: :polls
  has_many :options, dependent: :destroy
  has_many :participants, dependent: :destroy
  has_many :comments, dependent: :destroy
  has_many :votes, dependent: :destroy
  has_many :invites, class_name: "PollInvite", inverse_of: :poll, dependent: :destroy
  has_many :activities, class_name: "PollActivity", inverse_of: :poll, dependent: :destroy
end
