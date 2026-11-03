defmodule Teable.BaseShare do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t(),
          share_id: String.t(),
          password: String.t() | nil,
          node_id: String.t() | nil,
          allow_save: boolean() | nil,
          allow_copy: boolean() | nil,
          allow_edit: boolean() | nil,
          enabled: boolean(),
          created_time: DateTime.t(),
          created_by: String.t(),
          updated_at: DateTime.t() | nil
        }

  schema "base_share" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:share_id, :string)
    field(:password, :string)
    field(:node_id, :string)
    field(:allow_save, :boolean)
    field(:allow_copy, :boolean)
    field(:allow_edit, :boolean)
    field(:enabled, :boolean, default: true)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end