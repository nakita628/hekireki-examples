defmodule Jitsu.ProfileBuilderFunction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          profile_builder: Jitsu.ProfileBuilder.t() | nil,
          function: Jitsu.ConfigurationObject.t() | nil
        }

  schema "ProfileBuilderFunction" do
    field(:id, :string, primary_key: true)
    field(:profile_builder_id, :string, source: :profileBuilderId)
    field(:function_id, :string, source: :functionId)
    belongs_to(:profile_builder, Jitsu.ProfileBuilder, foreign_key: :profile_builder_id, define_field: false, type: :string)
    belongs_to(:function, Jitsu.ConfigurationObject, foreign_key: :function_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end