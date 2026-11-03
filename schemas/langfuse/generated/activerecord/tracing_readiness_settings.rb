class TracingReadinessSettings < ApplicationRecord
  self.primary_key = "project_id"

  belongs_to :project
  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :tracing_readiness_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :tracing_readiness_updated
end
