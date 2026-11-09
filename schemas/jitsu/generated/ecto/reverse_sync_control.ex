defmodule Jitsu.ReverseSyncControl do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          sync_id: String.t(),
          run_id: String.t(),
          run_order: integer(),
          detached: boolean(),
          revision: String.t(),
          target_hash: String.t(),
          phase: String.t(),
          mode: atom(),
          extraction: atom(),
          artifact_head: binary() | nil,
          base_sequence: integer(),
          next_sequence: integer(),
          checkpoint_sequence: integer(),
          finish_sequence: integer() | nil,
          store: binary() | nil,
          provider_state: binary() | nil,
          finish_result: binary() | nil,
          committed_generation: String.t() | nil
        }

  schema "reverse_sync_control" do
    field(:workspace_id, :string, primary_key: true)
    field(:sync_id, :string, primary_key: true)
    field(:run_id, :string, primary_key: true)
    field(:run_order, :integer, default: 0)
    field(:detached, :boolean, default: false)
    field(:revision, :string)
    field(:target_hash, :string)
    field(:phase, :string, default: "new")
    field(:mode, Ecto.Enum, values: [:upsert, :mirror])
    field(:extraction, Ecto.Enum, values: [:cursor, :full])
    field(:artifact_head, :binary)
    field(:base_sequence, :integer, default: 0)
    field(:next_sequence, :integer, default: 0)
    field(:checkpoint_sequence, :integer, default: 0)
    field(:finish_sequence, :integer)
    field(:store, :binary)
    field(:provider_state, :binary)
    field(:finish_result, :binary)
    field(:committed_generation, :string)
  end
end