class ProfileBuilderFunction < ApplicationRecord
  self.table_name = "ProfileBuilderFunction"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :profile_builder, foreign_key: "profileBuilderId", inverse_of: :functions
  belongs_to :function, class_name: "ConfigurationObject", foreign_key: "functionId",
                        inverse_of: :profile_builder_function
end
