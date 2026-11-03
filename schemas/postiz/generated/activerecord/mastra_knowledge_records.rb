class MastraKnowledgeRecords < ApplicationRecord
  validates :node, presence: true
  validates :text, presence: true
  validates :scope, exclusion: { in: [ nil ] }
  validates :scopeKey, presence: true
  validates :sourceThreadId, presence: true
  validates :capturedAt, presence: true
end
