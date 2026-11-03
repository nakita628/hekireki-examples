class InAppAgentRun < ApplicationRecord
  self.primary_key = %w[id project_id]

  belongs_to :project, inverse_of: :in_app_agent_run
  belongs_to :conversation, class_name: "InAppAgentConversation", foreign_key: %i[conversation_id project_id],
                            inverse_of: :runs
  belongs_to :triggered_by_user, class_name: "User", optional: true, inverse_of: :in_app_agent_runs
  has_many :events, class_name: "InAppAgentEvent", foreign_key: %i[run_id project_id], inverse_of: :run,
                    dependent: :destroy
end
