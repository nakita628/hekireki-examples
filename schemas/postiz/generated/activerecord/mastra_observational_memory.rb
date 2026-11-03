class MastraObservationalMemory < ApplicationRecord
  self.table_name = "mastra_observational_memory"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :lookupKey, presence: true
  validates :scope, presence: true
  validates :activeObservations, presence: true
  validates :originType, presence: true
  validates :config, presence: true
  validates :generationCount, presence: true
  validates :pendingMessageTokens, presence: true
  validates :totalTokensObserved, presence: true
  validates :observationTokenCount, presence: true
  validates :isObserving, inclusion: { in: [ true, false ] }
  validates :isReflecting, inclusion: { in: [ true, false ] }
  validates :isBufferingObservation, inclusion: { in: [ true, false ] }
  validates :isBufferingReflection, inclusion: { in: [ true, false ] }
  validates :lastBufferedAtTokens, presence: true
end
