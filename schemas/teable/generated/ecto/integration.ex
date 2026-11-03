defmodule Teable.Integration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          resource_id: String.t(),
          config: String.t(),
          type: String.t(),
          enable: boolean() | nil,
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil
        }

  schema "integration" do
    field(:id, :string, primary_key: true)
    field(:resource_id, :string)
    field(:config, :string)
    field(:type, :string)
    field(:enable, :boolean)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end