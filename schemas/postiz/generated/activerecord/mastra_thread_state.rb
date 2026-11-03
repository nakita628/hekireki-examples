class MastraThreadState < ApplicationRecord
  self.table_name = "mastra_thread_state"
  self.primary_key = %w[threadId type]
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :value, exclusion: { in: [ nil ] }
end
