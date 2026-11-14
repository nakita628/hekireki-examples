class SessionViewPinOverride < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :is_pinned, default: true

  validates :project_id, uniqueness: { scope: %i[user_id view_id], conditions: -> { where.not(view_id: nil) } }
  validates :position, presence: true

  belongs_to :project, inverse_of: :session_view_pin_overrides
  belongs_to :user, inverse_of: :session_view_pin_overrides
  belongs_to :view, class_name: "SessionView", foreign_key: %i[project_id view_id], primary_key: %i[project_id id],
                    optional: true, inverse_of: :pins
end
