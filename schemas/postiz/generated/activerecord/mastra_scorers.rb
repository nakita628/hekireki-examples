class MastraScorers < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :scorerId, presence: true
  validates :runId, presence: true
  validates :scorer, exclusion: { in: [ nil ] }
  validates :score, presence: true
  validates :input, exclusion: { in: [ nil ] }
  validates :output, exclusion: { in: [ nil ] }
  validates :source, presence: true
end
