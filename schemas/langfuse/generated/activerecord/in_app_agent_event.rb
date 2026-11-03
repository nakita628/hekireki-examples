class InAppAgentEvent < ApplicationRecord
  self.primary_key = %w[project_id conversation_id sequence_number]
  self.inheritance_column = nil

  validates :type, presence: true
  validates :event, exclusion: { in: [ nil ] }

  belongs_to :project, inverse_of: :in_app_agent_event
  belongs_to :conversation, class_name: "InAppAgentConversation", foreign_key: %i[conversation_id project_id],
                            inverse_of: :events
  belongs_to :run, class_name: "InAppAgentRun", foreign_key: %i[run_id project_id], inverse_of: :events
end
