class ViewerGroup < ApplicationRecord
  self.table_name = "ViewerGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :domains, default: -> { [] }
  attribute :allowAll, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :viewer_groups
  belongs_to :team, foreign_key: "teamId", inverse_of: :viewer_groups
  has_many :members, class_name: "ViewerGroupMembership", foreign_key: "groupId", inverse_of: :group,
                     dependent: :destroy
  has_many :links, foreign_key: "groupId", inverse_of: :group, dependent: :nullify
  has_many :access_controls, class_name: "ViewerGroupAccessControls", foreign_key: "groupId", inverse_of: :group,
                             dependent: :destroy
  has_many :views, foreign_key: "groupId", inverse_of: :group, dependent: :nullify
  has_many :conversations, foreign_key: "viewerGroupId", inverse_of: :viewer_group, dependent: :nullify
  has_many :invitations, class_name: "ViewerInvitation", foreign_key: "groupId", inverse_of: :group, dependent: :nullify
  has_many :question_assignments, class_name: "DataroomQuestionAssignment", foreign_key: "groupId", inverse_of: :group,
                                  dependent: :destroy
  has_many :task_assignments, foreign_key: "groupId", inverse_of: :group, dependent: :destroy
end
