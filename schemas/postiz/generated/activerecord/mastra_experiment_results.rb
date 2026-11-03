class MastraExperimentResults < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :experimentId, presence: true,
                           uniqueness: { scope: [ :itemId, :attempt ], conditions: -> { where.not(attempt: nil) } }
  validates :itemId, presence: true
  validates :input, exclusion: { in: [ nil ] }
  validates :startedAt, presence: true
  validates :completedAt, presence: true
  validates :retryCount, presence: true
end
